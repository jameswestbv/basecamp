using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class ProjectManager : ManagerBase
    {
        public ProjectManager(Api api) : base(api)
        {

        }

        public IEnumerable<ProjectStub> GetActive()
        {
            var response = Api.Get<IEnumerable<ProjectStub>>("projects.json");
            return response;
        }

        public IEnumerable<ProjectStub> GetArchived()
        {
            var response = Api.Get<IEnumerable<ProjectStub>>("projects/archived.json");
            return response;
        }

        public Project Get(ProjectStub project)
        {
            return Get(project.Id);
        }

        public Project Get(int projectId)
        {
            var response = Api.Get<Project>("projects/{0}.json".FormatWith(projectId));
            return response;
        }

        public bool Delete(ProjectStub project)
        {
            return Delete(project.Id);
        }

        public bool Delete(int projectId)
        {
            var action = "/projects/{0}.json".FormatWith(projectId);
            return Api.Delete(action);
        }

        public void Create(Project project)
        {
            var request = new ProjectCreateRequest();
            ModelCopier.CopyModel(project, request);
            var createdProject = Create(request);
            project.Api = Api;
            ModelCopier.CopyModel(createdProject, project);
        }

        public Project Create(ProjectCreateRequest createRequest)
        {
            var content = Api.Post<Project>("/projects.json", createRequest);
            return content;
        }

        public bool Update(ProjectStub project)
        {
            var updatedProject = Api.Put<Project>("/projects/{0}.json".FormatWith(project.Id), project);

            if (updatedProject != null)
            {
                ModelCopier.CopyModel(updatedProject, project);
                return true;
            }

            return false;
        }
    }
}
