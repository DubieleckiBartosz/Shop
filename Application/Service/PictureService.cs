using Application.DtoModels.PictureModels;
using Application.Service.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Extensions;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Service
{
    public class PictureService: IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<PictureService> _logger;
        public PictureService(IPictureRepository pictureRepository, IMapper mapper, IProductRepository productRepository,ILogger<PictureService> logger)
        {
            _productRepository = productRepository;
            _pictureRepository = pictureRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<PictureDto> AddPictureToProductAsync(int productId, IFormFile file)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product is null) throw new NotFoundException("Not found product");

            var existPictures = await _pictureRepository.GetByIdProductAsync(productId);
            var picture = new Picture()
            {
                Products = new List<Product> { product },
                Name = file.FileName,
                Image = file.GetBytes(),
                Main = existPictures.Count()==0 ?true : false
            };
            var result = await _pictureRepository.AddPictureAsync(picture);
            _logger.LogInformation($"Added new picture to product with id: {productId}");
            return _mapper.Map<PictureDto>(result);
        }

        public async Task DeletePictureAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            if (picture is null) throw new NotFoundException("Not found picture");
            _logger.LogInformation($"Deleted picture with id: {id}");
            await _pictureRepository.DeleteAsync(picture);
        }

        public async Task<PictureDto> GetPictureByIdAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            if (picture is null) throw new NotFoundException("Picture not found");
            return _mapper.Map<PictureDto>(picture);
        }

        public async Task<IEnumerable<PictureDto>> GetPicturesByProductIdAsync(int productId)
        {
            var pictures = await _pictureRepository.GetByIdProductAsync(productId);
            if (pictures is null) throw new NotFoundException("Not found pictures");
            return _mapper.Map<IEnumerable<PictureDto>>(pictures);
        }

        public async Task SetMainPicture(int productId, int id)
        {
            await _pictureRepository.SetMainPictureAsync(productId, id);
        }
    }
}
