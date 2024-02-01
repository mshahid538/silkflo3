// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IDocumentRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IDocumentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Document> GetAsync(string id);

    Task<Document> SingleOrDefaultAsync(Func<Document, bool> predicate);

    Task<bool> AddAsync(Document entity);

    Task<bool> AddRangeAsync(IEnumerable<Document> entities);

    Task<IEnumerable<Document>> GetAllAsync();

    Task<IEnumerable<Document>> FindAsync(Func<Document, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Document entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Document> entities);
  }
}
