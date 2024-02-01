// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IRecipientRepository
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
  public interface IRecipientRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Recipient> GetAsync(string id);

    Task<Recipient> SingleOrDefaultAsync(Func<Recipient, bool> predicate);

    Task<bool> AddAsync(Recipient entity);

    Task<bool> AddRangeAsync(IEnumerable<Recipient> entities);

    Task<IEnumerable<Recipient>> GetAllAsync();

    Task<IEnumerable<Recipient>> FindAsync(Func<Recipient, bool> predicate);

    Task GetForCommentAsync(Comment comment);

    Task GetForCommentAsync(IEnumerable<Comment> comments);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Recipient entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Recipient> entities);
  }
}
