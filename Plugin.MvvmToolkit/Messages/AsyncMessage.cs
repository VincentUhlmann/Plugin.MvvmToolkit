﻿namespace Plugin.MvvmToolkit.Messages;

/// <summary>
/// A <see langword="class"/> for async messages, which can either be used directly or through derived classes.
/// </summary>
public class AsyncMessage
{
    private Task? response;

    /// <summary>
    /// Gets the message response.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="HasReceivedResponse"/> is <see langword="false"/>.</exception>
    public Task Response {
        get {
            if (!HasReceivedResponse) {
                ThrowInvalidOperationExceptionForNoResponseReceived();
            }

            return response!;
        }
    }

    /// <summary>
    /// Gets a value indicating whether a response has already been assigned to this instance.
    /// </summary>
    public bool HasReceivedResponse { get; private set; }

    /// <summary>
    /// Replies to the current message.
    /// </summary>
    /// <param name="response">The response to use for replying to the message.</param>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="Response"/> has already been set.</exception>
    public void Reply()
    {
        Reply(Task.CompletedTask);
    }

    /// <summary>
    /// Replies to the current message.
    /// </summary>
    /// <param name="response">The response to use for replying to the message.</param>
    /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="response"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="Response"/> has already been set.</exception>
    public void Reply(Task response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (HasReceivedResponse) {
            ThrowInvalidOperationExceptionForDuplicateReply();
        }

        HasReceivedResponse = true;

        this.response = response;
    }

    /// <inheritdoc cref="Task.GetAwaiter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public TaskAwaiter GetAwaiter()
    {
        return Response.GetAwaiter();
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> when a response is not available.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowInvalidOperationExceptionForNoResponseReceived()
    {
        throw new InvalidOperationException("No response was received for the given message.");
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> when <see cref="Reply()"/> or <see cref="Reply(Task)"/> are called twice.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowInvalidOperationExceptionForDuplicateReply()
    {
        throw new InvalidOperationException("A response has already been issued for the current message.");
    }
}
