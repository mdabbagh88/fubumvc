﻿using FubuPersistence.Storage;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuPersistence.Tests.Storage
{
    [TestFixture]
    public class when_finding_the_entity_storage_with_no_matching_policies : InteractionContext<StorageFactory>
    {
        protected override void beforeEach()
        {
            MockFor<IEntityStoragePolicy>().Stub(x => x.Matches<User>()).Return(false);
        }

        [Test]
        public void uses_the_global_entity_storage_by_default()
        {
            ClassUnderTest.StorageFor<User>().ShouldBeOfType<EntityStorage<User>>();
        }
    }
}