using System.Collections.Generic;

namespace IQuality.Models.Chat.Messages.Graph
{
    public class GraphData
    {
        public string Title { get; set; }
        public GraphOptions Options { get; set; }
        public List<GraphEntry> Entries { get; set; }
    }
}