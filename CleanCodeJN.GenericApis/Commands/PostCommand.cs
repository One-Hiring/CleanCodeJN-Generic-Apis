﻿using AutoMapper;
using CleanCodeJN.GenericApis.Abstractions.Responses;
using CleanCodeJN.Repository.EntityFramework.Contracts;
using MediatR;

namespace CleanCodeJN.GenericApis.Commands;

public class PostCommand<TEntity, TDto, TKey>(IMapper mapper, IRepository<TEntity, TKey> repository) : IRequestHandler<PostRequest<TEntity, TDto>, BaseResponse<TEntity>>
    where TEntity : class, IEntity<TKey>
{
    public async Task<BaseResponse<TEntity>> Handle(PostRequest<TEntity, TDto> request, CancellationToken cancellationToken)
    {
        var entity = await repository.Create(mapper.Map<TEntity>(request.Dto), cancellationToken);

        return await BaseResponse<TEntity>.Create(entity is not null, entity);
    }
}
