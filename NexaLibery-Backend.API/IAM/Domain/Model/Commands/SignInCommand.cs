﻿namespace NexaLibery_Backend.API.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign in command
 * </summary>
 * <remarks>
 *     This command object includes the username and password to sign in
 * </remarks>
 */
public record SignInCommand(string Email, string Password);