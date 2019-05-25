using System;
using System.Collections.Generic;

namespace Hack.Service
{
    // Dto extensions are used to avoid name collisions.

    public sealed class GetTasksResponse : SuccessResponse
    {
        public int Total { get; set; }
        public IEnumerable<JiraIssueDto> Issues { get; set; }

        public sealed class JiraIssueDto
        {
            public string Id { get; set; }
            public string Key { get; set; }
            public FieldsDto Fields { get; set; }

            public sealed class FieldsDto
            {
                public IssueTypeDto IssueType { get; set; }
                public float? TimeSpent { get; set; }
                public ResolutionDto Resolution { get; set; }
                public DateTime Created { get; set; }
                public PriorityDto Priority { get; set; }
                public TimeEstimateDto TimeEstimate { get; set; }
                public AssigneeDto Assignee { get; set; }
                public DateTime Updated { get; set; }
                public string Summary { get; set; }
                public UserDto Creator { get; set; }
                public UserDto Reporter { get; set; }
                public ProgressDto AggregateProgress { get; set; }

                #region SubItems.FieldDto

                public sealed class IssueTypeDto
                {
                    public string Name { get; set; }
                    public bool Subtask { get; set; }
                }

                public sealed class ResolutionDto
                {
                }

                public sealed class PriorityDto
                {
                    public int Id { get; set; }
                    public string Name { get; set; }
                }

                public sealed class TimeEstimateDto
                {
                }

                public sealed class AssigneeDto
                {
                }

                public sealed class StatusDto
                {
                    public string Description { get; set; }
                    public string Name { get; set; }
                    public int Id { get; set; }
                    public StatusCategoryDto StatusCategory { get; set; }

                    public sealed class StatusCategoryDto
                    {
                        public int Id { get; set; }
                        public string Key { get; set; }
                        public string Name { get; set; }
                    }
                }

                public sealed class UserDto
                {
                    public string Name { get; set; }
                    public string Key { get; set; }
                    public string AccountId { get; set; }
                    public string EmailAddress { get; set; }
                    public dynamic AvatarUrls { get; set; }
                    public string DisplayName { get; set; }
                    public bool Active { get; set; }
                }

                public sealed class ProgressDto
                {
                    public float Progress { get; set; }
                    public float Total { get; set; }
                }

                #endregion SubItems.FieldDto
            }
        }
    }
}