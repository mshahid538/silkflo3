// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.ICommentRepository
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
  public interface ICommentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Comment> GetAsync(string id);

    Task<Comment> SingleOrDefaultAsync(Func<Comment, bool> predicate);

    Task<bool> AddAsync(Comment entity);

    Task<bool> AddRangeAsync(IEnumerable<Comment> entities);

    Task<IEnumerable<Comment>> GetAllAsync();

    Task<IEnumerable<Comment>> FindAsync(Func<Comment, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForSenderAsync(User sender);

    Task GetForSenderAsync(IEnumerable<User> senders);

    Task GetCommentForAsync(Recipient recipient);

    Task GetCommentForAsync(IEnumerable<Recipient> recipients);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Comment entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Comment> entities);
  }
}
