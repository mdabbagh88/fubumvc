﻿using System.Collections.Generic;
using System.Linq;
using FubuMVC.PersistedMembership;
using FubuPersistence;
using StoryTeller;

namespace Specifications.Fixtures
{
    public class LoginAuditingFixture : Fixture
    {
        public LoginAuditingFixture()
        {
            Title = "Login Auditing";
        }

        public IGrammar TheAuditsAre()
         {
             return VerifySetOf(allAudits).Titled("All of the audit messages should be")
                 .Ordered()                         
                 .MatchOn(x => x.Username, x => x.Type);
         }

        private IEnumerable<Audit> allAudits()
        {
            return Context.Service<IEntityRepository>().All<Audit>()
                                                .OrderBy(x => x.Timestamp);
        }
    }
}