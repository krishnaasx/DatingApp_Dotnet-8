namespace API.Interface {
    public interface IUnitOfWork {
        
        IUserRespository UserRespository { get;}
        IMessageRepository MessageRepository { get;}
        ILikesRepository LikesRepository{ get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}