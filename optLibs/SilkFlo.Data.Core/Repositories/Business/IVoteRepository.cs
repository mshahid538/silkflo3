// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IVoteRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IVoteRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Vote> GetAsync(string id);

    Task<Vote> SingleOrDefaultAsync(Func<Vote, bool> predicate);

    Task<bool> AddAsync(Vote entity);

    Task<bool> AddRangeAsync(IEnumerable<Vote> entities);

    Task<IEnumerable<Vote>> GetAllAsync();

    Task<IEnumerable<Vote>> FindAsync(Func<Vote, bool> predicate);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Vote entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Vote> entities);
  }
}
