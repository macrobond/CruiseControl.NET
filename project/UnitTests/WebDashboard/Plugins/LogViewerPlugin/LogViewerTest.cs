using NMock;
using NUnit.Framework;
using ThoughtWorks.CruiseControl.WebDashboard.Cache;
using ThoughtWorks.CruiseControl.WebDashboard.Dashboard;
using ThoughtWorks.CruiseControl.WebDashboard.IO;
using ThoughtWorks.CruiseControl.WebDashboard.Plugins.LogViewerPlugin;

namespace ThoughtWorks.CruiseControl.UnitTests.WebDashboard.Plugins.LogViewerPlugin
{
	[TestFixture]
	public class LogViewerTest : Assertion
	{
		private LogViewer logViewer;

		private string url;
		private Build build;
		private DynamicMock buildRetrieverforRequestMock;
		private DynamicMock requestWrapperMock;
		private DynamicMock cacheManagerMock;
		private string serverName;
		private string projectName;

		[SetUp]
		public void Setup()
		{
			buildRetrieverforRequestMock = new DynamicMock(typeof(IBuildRetrieverForRequest));
			requestWrapperMock = new DynamicMock(typeof(IRequestWrapper));
			cacheManagerMock = new DynamicMock(typeof(ICacheManager));

			logViewer = new LogViewer((IRequestWrapper) requestWrapperMock.MockInstance, (IBuildRetrieverForRequest) buildRetrieverforRequestMock.MockInstance,
				(ICacheManager) cacheManagerMock.MockInstance);

			url = "http://foo.bar";
			serverName = "myserver";
			projectName = "myproject";
			build  = new Build("mybuild", "", serverName, projectName);
		}

		private void VerifyAll()
		{
			buildRetrieverforRequestMock.Verify();
			requestWrapperMock.Verify();
			cacheManagerMock.Verify();
		}

		[Test]
		public void ReturnsURLOfRelevantBuild()
		{
			IRequestWrapper requestWrapper = (IRequestWrapper) requestWrapperMock.MockInstance;

			buildRetrieverforRequestMock.ExpectAndReturn("GetBuild", build, requestWrapper);
			cacheManagerMock.ExpectAndReturn("GetURLForFile", url, serverName, projectName, CachingBuildRetriever.CacheDirectory, "mybuild");

			AssertEquals(url, logViewer.Do().RedirectURL);
		}
	}
}
