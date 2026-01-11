using Application.Abstractions;
using Application.Common.Extensions;
using Application.Tasks.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.Services
{
    public class GetTasksQuery : IRequest<List<TaskDto>>
    {
        public string? Search { get; set; }
        public Status? Status { get; set; }
        public TaskType? Type { get; set; }
        public Priority? Priority { get; set; }
    }

    public class GetTasksQueryHandler
        : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTasksQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDto>> Handle(
            GetTasksQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Set<TaskItem>()
                .AsNoTracking()
                .WhatIf(!string.IsNullOrWhiteSpace(request.Search),
                    t => t.Title.Contains(request.Search!) ||
                         t.Description.Contains(request.Search!))

                .WhatIf(request.Status.HasValue,
                    t => t.Status == request.Status)

                .WhatIf(request.Type.HasValue,
                    t => t.Type == request.Type)

                .WhatIf(request.Priority.HasValue,
                    t => t.Priority == request.Priority);

            return await query
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    Type = t.Type,
                    Priority = t.Priority
                })
                .ToListAsync(cancellationToken);
        }
    }
}
