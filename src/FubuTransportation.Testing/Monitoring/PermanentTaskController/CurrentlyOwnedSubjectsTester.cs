﻿using System.Linq;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuTransportation.Testing.Monitoring.PermanentTaskController
{
    [TestFixture]
    public class CurrentlyOwnedSubjectsTester : PersistentTaskControllerContext
    {
        [Test]
        public void includes_both_active_subjects_and_subjects_persisted_as_owned()
        {
            Task("foo://1").IsFullyFunctionalAndActive();
            Task("foo://2").IsFullyFunctionalAndActive();
            Task("foo://3").IsFullyFunctionalAndActive();

            theCurrentNode.OwnedTasks = new[] { "foo://1".ToUri(), "foo://2".ToUri(), "foo://4".ToUri() };

            theController.CurrentlyOwnedSubjects().OrderBy(x => x.ToString()).ShouldHaveTheSameElementsAs(
                "foo://1".ToUri(), "foo://2".ToUri(),"foo://3".ToUri(), "foo://4".ToUri()
                );
        }
    }
}