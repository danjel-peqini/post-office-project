using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class GroupDomain : DomainBase, IGroupDomain
    {
        public GroupDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IGroupRepository GroupRepository => _unitOfWork.GetRepository<IGroupRepository>();
        private IGroupStudentRepository GroupStudentRepository => _unitOfWork.GetRepository<IGroupStudentRepository>();

        public void AddNew(GroupPostDTO group)
        {
            var entity = _mapper.Map<TblGroup>(group);
            entity.Id = Guid.NewGuid();
            GroupRepository.Add(entity);
            if (group.StudentIds != null && group.StudentIds.Any())
            {
                var uniqueIds = group.StudentIds.Distinct();
                var groupStudents = uniqueIds.Select(studentId => new TblGroupStudent
                {
                    Id = Guid.NewGuid(),
                    GroupId = entity.Id,
                    StudentId = studentId
                });
                GroupStudentRepository.AddRange(groupStudents);
            }
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            GroupRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Pagination<GroupDTO> GetAllGroups(QueryParameters queryParameters)
        {
            var groups = GroupRepository.GetGroups(queryParameters);
            var paginatedData = Pagination<GroupDTO>.ToPagedList(groups, _mapper.Map<List<GroupDTO>>);
            return paginatedData;
        }

        public GroupDTO GetGroupById(Guid id)
        {
            var entity = GroupRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Group not found");
            }
            return _mapper.Map<GroupDTO>(entity);
        }

        public GroupDTO Update(Guid id, GroupPostDTO group)
        {
            var entity = GroupRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Group not found");
            }
            if (!string.IsNullOrWhiteSpace(group.Name))
                entity.Name = group.Name;
            if (group.CourseId.HasValue)
                entity.CourseId = group.CourseId.Value;
            if (group.AcademicYearId.HasValue)
                entity.AcademicYearId = group.AcademicYearId.Value;
            GroupRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<GroupDTO>(entity);
        }

        public void AddStudents(Guid groupId, GroupStudentPostDTO dto)
        {
            var group = GroupRepository.GetById(groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            if (dto.StudentIds == null || !dto.StudentIds.Any())
                return;

            var existingIds = GroupStudentRepository
                .Find(x => x.GroupId == groupId && dto.StudentIds.Contains(x.StudentId))
                .Select(x => x.StudentId)
                .ToHashSet();

            var entities = dto.StudentIds
                .Where(id => !existingIds.Contains(id))
                .Distinct()
                .Select(id => new TblGroupStudent
                {
                    Id = Guid.NewGuid(),
                    GroupId = groupId,
                    StudentId = id
                })
                .ToList();

            if (!entities.Any())
                return;

            GroupStudentRepository.AddRange(entities);
            _unitOfWork.Save();
        }

        public void RemoveStudents(Guid groupId, GroupStudentPostDTO dto)
        {
            if (dto.StudentIds == null || !dto.StudentIds.Any())
                return;
            var entities = GroupStudentRepository
                .Find(x => x.GroupId == groupId && dto.StudentIds.Contains(x.StudentId))
                .ToList();
            GroupStudentRepository.RemoveRange(entities);
            _unitOfWork.Save();
        }

        public IEnumerable<Guid> GetStudents(Guid groupId)
        {
            return GroupStudentRepository.GetStudentIdsByGroupId(groupId);
        }
    }
}
