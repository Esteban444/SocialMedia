using FluentValidation;
using SocialMedia.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Validators
{
    public class PostValidator : AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(post => post.Descriptions)
                .NotNull()
                .WithMessage("La descripcion no puede ir vacia");

            RuleFor(post => post.Dates)
                .NotNull()
                .WithMessage("La Fecha no puede ir vacia");

            RuleFor(post => post.Descriptions)
                .Length(10, 500)
                .WithMessage("La descripcion debe tener entre 10 y 500 caracteres");
        }
    }
}
