# Turbulence
An alternative Discord client (and maybe more haha what if)

#### Features:
- Not Electron
- Multiple Frontends (Avalonia, Terminal GUI)
- Discord reminiscent UI (depends on the chosen Frontend)
- Supports most Discord text chat features
- Working Search functionality

Strap in, more is coming.

## Quick start guide

#### Get your authentication token
1. Open [Discord](https://discord.com/channels/@me) in any browser (you can also use the desktop client, but then you first have to do [this](https://www.reddit.com/r/discordapp/comments/sc61n3/comment/hu4fw5x/)).
2. Log in, if you are not already logged in.
3. Press <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>I</kbd> or <kbd>F12</kbd> to open the developer tools.
4. Go to the `Network` tab.
5. In the filter box, type `/api/v9`.
6. Refresh the page/client with <kbd>Ctrl</kbd> + <kbd>R</kbd> or <kbd>F5</kbd> (or open a new DM/channel).
7. Click any of the items in the list that just appeared.
8. On the right side, under `Headers`, find an entry called `authorization` (it should be under `Request Headers`).
9. Copy the value, this is your token.

![How to get the token](docs/img/token.png)

#### Use the token
1. In a terminal, go to the `Turbulence.Core` directory.
2. Run `dotnet user-secrets init` (if it's your first time setting user secrets).
3. Run `dotnet user-secrets set token [your token here]`.

Now you can run your favourite Turbulence frontend like `Turbulence.Desktop` or `Turbulence.TGUI`.

## Support
You can open a GitHub issue [here](https://github.com/TurbulenceDev/Turbulence/issues).