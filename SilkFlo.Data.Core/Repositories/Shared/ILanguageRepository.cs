// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.ILanguageRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface ILanguageRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Language> GetAsync(string id);

    Task<Language> SingleOrDefaultAsync(Func<Language, bool> predicate);

    Task<bool> AddAsync(Language entity);

    Task<bool> AddRangeAsync(IEnumerable<Language> entities);

    Task<IEnumerable<Language>> GetAllAsync();

    Task<IEnumerable<Language>> FindAsync(Func<Language, bool> predicate);

    Task<Language> GetUsingNameAsync(string name);

    Task GetLanguageForAsync(Client client);

    Task GetLanguageForAsync(IEnumerable<Client> clients);

    Task GetLanguageForAsync(IdeaApplicationVersion ideaApplicationVersion);

    Task GetLanguageForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions);

    Task<Language> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Language entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Language> entities);
  }
}
