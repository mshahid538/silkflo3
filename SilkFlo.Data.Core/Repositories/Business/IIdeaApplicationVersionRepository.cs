// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IIdeaApplicationVersionRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
    public interface IIdeaApplicationVersionRepository
    {
        bool IncludeDeleted { get; set; }

        Task<IdeaApplicationVersion> GetAsync(string id);

        Task<IdeaApplicationVersion> SingleOrDefaultAsync(Func<IdeaApplicationVersion, bool> predicate);

        Task<bool> AddAsync(IdeaApplicationVersion entity);

        Task<bool> AddRangeAsync(IEnumerable<IdeaApplicationVersion> entities);

        Task<IEnumerable<IdeaApplicationVersion>> GetAllAsync();

        Task<IEnumerable<IdeaApplicationVersion>> FindAsync(Func<IdeaApplicationVersion, bool> predicate);

        Task GetForIdeaAsync(Idea idea);

        Task GetForIdeaAsync(IEnumerable<Idea> ideas);

        Task GetForLanguageAsync(Language language);

        Task GetForLanguageAsync(IEnumerable<Language> languages);

        Task GetForVersionAsync(SilkFlo.Data.Core.Domain.Business.Version version);

        Task GetForVersionAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions);

        Task<DataStoreResult> RemoveAsync(string id);

        Task<DataStoreResult> RemoveAsync(IdeaApplicationVersion entity);

        Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaApplicationVersion> entities);
    }
}