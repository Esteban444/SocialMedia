using SocialMedia.Core.Interfaces;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;
using System.Linq;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.CustonEntities;
using Microsoft.Extensions.Options;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        //private readonly IPostRepository _postRepository; // asi para repositorio individual
        //private readonly IUserRepository _UserRepository; // asi repositorio individual

        //private readonly IRepository<Post> _postRepository; // con generic repository
        //private readonly IRepository<Users> _UserRepository; // con generic repository
        private readonly IUnitOfWork _unitOfWork ;
        private readonly PaginationOptions _paginationOptions; // para valores por defecto de paginacion

        //lo comentado en el parentisis del constructor es repository con generic
        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options/*IRepository<Post> postRepository, IRepository<Users> userRepository*/)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            //_postRepository = postRepository;
            //_UserRepository = userRepository;
        }
        /*IEnumerable<Post> es cambiado en paginacion por pagedList<Post>*/
        public PagedList<Post> GetPosts(PostQueryFilter filters)// parametro agregado para el filtro de busqueda.
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            // las dos lineas anteriores es para definir por defecto los valores de PageNumber y PageSize

            var posts = _unitOfWork.PostRepository.GetAll(); 
            if(filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }

            if (filters.Dates != null)
            {
                posts = posts.Where(x => x.Dates.ToShortDateString() == filters.Dates?.ToShortDateString());
            }

            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Descriptions.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize); //Linea para la paginacion
            return pagedPosts;
        }


        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

       
        public async Task PostCrear(Post post)
        {
            //validacion  Solo se permite publicacion a un usuario previamente registrado
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }
            // Validacion si el usuario tiene menos de 10 publicaciones, solo puede publicar una vez por semana
            var userpost = await _unitOfWork.PostRepository.GetPostsByUsers(post.UserId);
            if(userpost.Count() < 10)
            {
                var lastpost = userpost.OrderByDescending(x => x.Dates).FirstOrDefault();
                if((post.Dates - lastpost.Dates).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }
            // No es permitido publicar informacion que haga referencia al sexo
            if (post.Descriptions.Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }
           await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChagesAsync();
        }

        public async Task<bool> PostUpdate(Post post)
        {
            var existpost = await _unitOfWork.PostRepository.GetById(post.Id);
            existpost.Descriptions = post.Descriptions;
            existpost.Images = post.Images;

            _unitOfWork.PostRepository.Update(existpost); // la variable existpost tiene los campos que solo se permiten actualizar
            await _unitOfWork.SaveChagesAsync();
            return true;
        }

        public async Task<bool> PostDelete(int id)  
        {
            await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChagesAsync();// agregue yo porque no me estaba eliminando
            return true;
        }

    }
}
