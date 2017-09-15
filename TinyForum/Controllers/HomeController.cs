﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using TinyForum.Helpers;
using TinyForum.Interfaces;
using TinyForum.Models;
using TinyForum.ViewModels;

namespace TinyForum.Controllers
{
    public class HomeController : Controller
    {
        private ITopicRepository _topic;
        private IRepository<TopicNode> _node;
        public UserManager<User> UserManager { get; }
        public HomeController(ITopicRepository topic, IRepository<TopicNode> node, UserManager<User> userManager)
        {
            _topic = topic;
            _node = node;
            UserManager = userManager;
        }

        public IActionResult Index([FromServices]IUserServices user)
        {
            var pagesize = 20;
            var pageindex = 1;
            Page<Topic> result = null;
            if (!string.IsNullOrEmpty(Request.Query["page"]))
                pageindex = Convert.ToInt32(Request.Query["page"]);
            if (!string.IsNullOrEmpty(Request.Query["s"]))
                result = _topic.PageList(r => r.Title.Contains(Request.Query["s"]), pagesize, pageindex);
            else
                result = _topic.PageList(pagesize, pageindex);
            ViewBag.Topics = result.List.Select(r => new TopicViewModel
            {
                Id = r.Id,
                NodeId = r.Node.Id,
                NodeName = r.Node.Name,
                UserName = r.User.UserName,
                Avatar = r.User.Avatar,
                Title = r.Title,
                Top = r.Top,
                Type = r.Type,
                ReplyCount = r.ReplyCount,
                LastReplyTime = r.LastReplyTime,
                CreateOn = r.CreateOn
            }).ToList();
            ViewBag.PageIndex = pageindex;
            ViewBag.PageCount = result.GetPageCount();
            ViewBag.User = user.User.Result;
            var nodes = _node.List().ToList();
            ViewBag.Nodes = nodes;
            ViewBag.NodeListItem = nodes.Where(r => r.ParentId != 0).Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.CreateOn = DateTime.Now;
                topic.Type = TopicType.Normal;
                _topic.Add(topic);
            }
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "论坛声明";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "联系方式";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}