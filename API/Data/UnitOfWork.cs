using API.Interface;

namespace API.Data {
    public class UnitOfWork(DataContext context, IUserRespository userRespository, ILikesRepository likesRepository, IMessageRepository messageRepository) : IUnitOfWork {
        public IUserRespository UserRespository => userRespository;

        public IMessageRepository MessageRepository => messageRepository;

        public ILikesRepository LikesRepository => likesRepository;

        public async Task<bool> Complete() {
            return await context.SaveChangesAsync() > 0;
        }

        public bool HasChanges() {
            return context.ChangeTracker.HasChanges();
        }
    }
}