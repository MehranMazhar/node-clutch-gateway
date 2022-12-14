using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace NodeClutchGateway.Domain.Blockchain;
public class Block : AuditableEntity, IAggregateRoot
{
    public Guid? ParentBlockId { get; private set; }
    public virtual Block ParentBlock { get; set; }
    public string Hash { get; private set; }
    public virtual ICollection<Transaction> Transactions { get; private set; }

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
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

}
