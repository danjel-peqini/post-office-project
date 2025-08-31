using System;
using System.Collections.Generic;
using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IGroupDomain
    {
        Pagination<GroupDTO> GetAllGroups(QueryParameters queryParameters, Guid? studentId);
        GroupDTO GetGroupById(Guid id);
        void AddNew(GroupPostDTO group);
        GroupDTO Update(Guid id, GroupPostDTO group);
        void Delete(Guid id);
        void AddStudents(Guid groupId, GroupStudentPostDTO dto);
        void RemoveStudents(Guid groupId, GroupStudentPostDTO dto);
        IEnumerable<Guid> GetStudents(Guid groupId);
    }
}
