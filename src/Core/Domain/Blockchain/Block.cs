﻿using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace NodeClutchGateway.Domain.Blockchain;
public class Block : AuditableEntity, IAggregateRoot
{
    public Guid? ParentBlockId { get; private set; }
    public virtual Block ParentBlock { get; set; }
    public string Hash { get; private set; }
    public ICollection<Transaction> Transactions { get; private set; }

    public Block(Guid parentBlockId, List<Transaction> transactions)
    {
        ParentBlockId = parentBlockId;
        Transactions = transactions;
        Hash = CreateHash();
    }

    public Block()
    {

    }

    private string CreateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string rawData = ParentBlockId.ToString() + CreatedOn + Transactions;
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Encoding.Default.GetString(bytes);
        }
    }

}
