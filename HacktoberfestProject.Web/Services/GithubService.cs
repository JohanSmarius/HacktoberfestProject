﻿using HacktoberfestProject.Web.Models;
using Microsoft.Extensions.Logging;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HacktoberfestProject.Web.Services
{
	public class GithubService
	{
		private ILogger<GithubService> _logger;
		private GitHubClient _client = new GitHubClient(new ProductHeaderValue("HacktoberfestProject"));

		public GithubService(ILogger<GithubService> logger)
		{
			_logger = logger;
		}

		public async  Task<List<Models.Repository>> GetRepos(string owner)
		{
			var repositories = await _client.Repository.GetAllForUser(owner);

			return repositories.Select(r => new Models.Repository(owner, r.Name, r.Url)).ToList();
		}

		public async Task<List<Pr>> GetPullRequestsForRepo(string owner, string name)
		{
			var prs = await _client.PullRequest.GetAllForRepository(owner, name);

			return prs.Select(pr => new Pr(pr.Number, pr.Url)).ToList();
		}
	}
}
