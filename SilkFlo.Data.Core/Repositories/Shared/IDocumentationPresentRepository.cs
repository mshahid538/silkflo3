// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IDocumentationPresentRepository
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
  public interface IDocumentationPresentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<DocumentationPresent> GetAsync(string id);

    Task<DocumentationPresent> SingleOrDefaultAsync(Func<DocumentationPresent, bool> predicate);

    Task<bool> AddAsync(DocumentationPresent entity);

    Task<bool> AddRangeAsync(IEnumerable<DocumentationPresent> entities);

    Task<IEnumerable<DocumentationPresent>> GetAllAsync();

    Task<IEnumerable<DocumentationPresent>> FindAsync(Func<DocumentationPresent, bool> predicate);

    Task<DocumentationPresent> GetUsingNameAsync(string name);

    Task GetDocumentationPresentForAsync(Idea idea);

    Task GetDocumentationPresentForAsync(IEnumerable<Idea> ideas);

    Task<DocumentationPresent> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(DocumentationPresent entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DocumentationPresent> entities);
  }
}
