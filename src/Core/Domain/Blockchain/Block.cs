using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace NodeClutchGateway.Domain.Blockchain;
public class Block : AuditableEntity, IAggregateRoot
{
    public string PreviousHash { get; private set; }
    public string Hash { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public Block(List<Transaction> transactions)
    {
        Transactions = transactions;
        Hash = CreateHash();
    }

    public Block()
    {

    }

    public string CreateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string rawData = PreviousHash + CreatedOn;// + Transactions;
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Encoding.Default.GetString(bytes);
        }
    }

}
