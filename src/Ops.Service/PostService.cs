﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ocuda.Ops.Models;
using Ocuda.Ops.Service.Filters;
using Ocuda.Ops.Service.Interfaces.Ops;
using Ocuda.Ops.Service.Models;

namespace Ocuda.Ops.Service
{
    public class PostService
    {
        private readonly InsertSampleDataService _insertSampleDataService;
        private readonly IPostRepository _postRepository;

        public PostService(InsertSampleDataService insertSampleDataService,
            IPostRepository postRepository)
        {
            _postRepository = postRepository 
                ?? throw new ArgumentNullException(nameof(postRepository));
            _insertSampleDataService = insertSampleDataService
                ?? throw new ArgumentNullException(nameof(insertSampleDataService));
        }

        public async Task<int> GetPostCountAsync()
        {
            return await _postRepository.CountAsync();
        }

        public async Task<ICollection<Post>> GetPostsAsync(int skip = 0, int take = 5)
        {
            // TODO modify this to do descending (most recent first)
            return await _postRepository.ToListAsync(skip, take, _ => _.CreatedAt);
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _postRepository.FindAsync(id);
        }

        public async Task<DataWithCount<ICollection<Post>>> GetPaginatedListAsync(BlogFilter filter)
        {
            return await _postRepository.GetPaginatedListAsync(filter);
        }

        public async Task<Post> CreateAsync(Post post)
        {
            post.CreatedAt = DateTime.Now;
            // TODO Set CreatedBy Id
            post.CreatedBy = 1;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveAsync();

            return post;
        }

        public async Task<Post> EditAsync(Post post)
        {
            // TODO check edit logic
            var currentPost = await _postRepository.FindAsync(post.Id);
            currentPost.Title = post.Title;
            currentPost.Content = post.Content;

            _postRepository.Update(currentPost);
            await _postRepository.SaveAsync();
            return post;
        }

        public async Task DeleteAsync(int id)
        {
            _postRepository.Remove(id);
            await _postRepository.SaveAsync();
        }
    }
}
