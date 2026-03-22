using Contracts;
using Typesense;

namespace SearchService.MessageHandlers;

public record AcceptAnswerHandler(ITypesenseClient client)
{
    public async Task HandleAsync(AnswerAccepted message)
    {
        await client.UpdateDocument("questions", message.QuestionId, new { HasAcceptedAnswer = true });
    }
}