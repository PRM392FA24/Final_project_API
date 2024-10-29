using BusinessObj.Models;
using DataAccessObj;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FeedbackRepository
    {
        private readonly GRACEFULLFLORISTContext _context;

        public FeedbackRepository(GRACEFULLFLORISTContext context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetByIdAsync(string id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<string> CreateAsync(Feedback feedback)
        {
            try
            {
                var newFeedback = new Feedback();

                newFeedback.FeedbackId = Guid.NewGuid().ToString("N").Substring(0, 10);
                newFeedback.Status = true;
                newFeedback.Description = feedback.Description;

                if (feedback.Point == null)
                {
                    if (feedback.ReplyId != null)
                    {
                        newFeedback.ReplyId = feedback.ReplyId; 
                    }
                    else
                    {
                        return "ERROR: ReplyId is required for a reply.";
                    }
                }
                else
                {
                    newFeedback.Point = feedback.Point;
                    newFeedback.ReplyId = null; 
                }
                await _context.Feedbacks.AddAsync(newFeedback);
                await _context.SaveChangesAsync();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> UpdateAsync(Feedback feedback)
        {
            try
            {
                var update = await this._context.Feedbacks.Where(x => x.FeedbackId.Equals(feedback.FeedbackId))
                                  .FirstOrDefaultAsync();
                if (update != null)
                {

                    update.Status = feedback.Status;
                    update.Description = feedback.Description ?? update.Description;

                    this._context.Feedbacks.Update(update);
                    await this._context.SaveChangesAsync();
                    return "SUCCESS";

                }
                return "FAIL";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<string> DeleteAsync(string id)
        {
            try
            {
                var delete = await this._context.Feedbacks.Where(x => x.FeedbackId.Equals(id))
                                 .FirstOrDefaultAsync();
                if (delete != null)
                {
                    if (delete.Status == false)
                    {
                        throw new Exception("NotFound");
                    }
                    delete.Status = false;
                    this._context.Feedbacks.Update(delete);
                    await _context.SaveChangesAsync();
                    return "SUCCESS";
                }
                return "FAIL";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
