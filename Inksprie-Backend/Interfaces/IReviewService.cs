﻿using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(int userId, ReviewDto reviewDto);

        Task<bool> HasUserReviewedAsync(int userId, int bookId);

    }
}
