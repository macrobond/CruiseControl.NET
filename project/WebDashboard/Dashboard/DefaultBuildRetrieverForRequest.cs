using ThoughtWorks.CruiseControl.WebDashboard.IO;
using ThoughtWorks.CruiseControl.WebDashboard.ServerConnection;

namespace ThoughtWorks.CruiseControl.WebDashboard.Dashboard
{
	public class DefaultBuildRetrieverForRequest : IBuildRetrieverForRequest
	{
		private readonly IBuildNameRetriever buildNameRetriever;
		private readonly IBuildRetriever buildRetriever;

		public DefaultBuildRetrieverForRequest(IBuildRetriever buildRetriever, IBuildNameRetriever buildNameRetriever)
		{
			this.buildRetriever = buildRetriever;
			this.buildNameRetriever = buildNameRetriever;
		}

		public Build GetBuild(IRequestWrapper requestWrapper)
		{
			string serverName = requestWrapper.GetServerName();
			string projectName = requestWrapper.GetProjectName();
			IBuildSpecifier buildSpecifier = requestWrapper.GetBuildSpecifier();

			return buildRetriever.GetBuild(serverName, projectName, GetBuildName(serverName, projectName, buildSpecifier));
		}

		private string GetBuildName(string serverName, string projectName, IBuildSpecifier buildSpecifier)
		{
			if (buildSpecifier is NoBuildSpecified)
			{
				return buildNameRetriever.GetLatestBuildName(serverName, projectName);
			}
			else
			{
				return ((NamedBuildSpecifier) buildSpecifier).Filename;
			}
		}
	}
}
