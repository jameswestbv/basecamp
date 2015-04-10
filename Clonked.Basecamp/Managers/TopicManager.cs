using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp.Managers
{
    public class TopicManager : ManagerBase
    {
        public TopicManager(Api api)  : base(api)
        {

        }

        public IEnumerable<Topic> GetTopicsForProject(int projectId)
        {
            return GetInner("/projects/{0}/topics.json".FormatWith(projectId));
        }

        public IEnumerable<Topic> GetTopics()
        {
            return GetInner("/topics.json");
        }

        private IEnumerable<Topic> GetInner(string action)
        {
            var allTopics = new List<Topic>();

            var events = Api.Get<IEnumerable<Topic>>(action);
            allTopics.AddRange(events);
            int page = 2;
            while (events.Count() == 50)
            {
                var nextPageAction = action + "&page=" + page++;
                events = Api.Get<IEnumerable<Topic>>(nextPageAction);
                allTopics.AddRange(events);
            }

            return allTopics;
        }
    }
}
