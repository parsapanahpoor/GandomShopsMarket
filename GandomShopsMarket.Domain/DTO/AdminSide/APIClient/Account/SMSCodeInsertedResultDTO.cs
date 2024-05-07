using GandomShopsMarket.Domain.Entities.Account;

namespace GandomShopsMarket.Domain.DTO.APIClient.Account;

public record SMSCodeInsertedResultDTO
{
    #region properties

    public User? User { get; set; }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    #endregion
}
