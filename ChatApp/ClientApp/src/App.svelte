<script>
  import { onMount } from "svelte";
  import { HubConnectionBuilder } from "@microsoft/signalr";

  let user = "defaultUser91";
  let newMessage;
  let messages = [];

  const MESSAGE_TYPES = Object.freeze({
    MESSAGE: "MESSAGE",
    ENTRANCE: "ENTRANCE"
  });

  function Message(user, message, type = MESSAGE_TYPES.MESSAGE) {
    this.user = user;
    this.message = message;
    this.type = type;
  }

  const connection = new HubConnectionBuilder().withUrl("/chatHub").build();

  connection.on("ReceiveMessage", (user, message) => {
    let msg = message
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;");
    messages = [...messages, new Message(user, msg)];
  });

  connection.on("EnterChatroom", user => {});

  connection.start();

  function handleSend() {
    connection.invoke("SendMessage", user, newMessage).catch(err => {
      return connection.error(err.toString());
    });
  }

  async function testCallApi() {
    const payload = {
      username: "username",
      password: "password"
    };

    const response = await fetch("/api/userauthentication/register", {
      method: "POST",
      body: JSON.stringify(payload),
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "same-origin"
    });
  }

  onMount(() => {
    connection.invoke("AlertEntrance", user);
    new Message(user, "", MESSAGE_TYPES.ENTRANCE);
    messages = [...messages, { user }];
  });
</script>

<style>
  .enter-chat {
    color: darkgreen;
  }
</style>

<div>User</div>
<input bind:value={user} />
<div>Message</div>
<input bind:value={newMessage} />
<button on:click={handleSend} type="button">Send</button>
<button on:click={testCallApi}>CAll API</button>

{#each messages as msg}
  {#if msg.type === 'MESSAGE'}
    <div>{msg.user} says: {msg.message}</div>
  {:else}
    <div class="enter-chat">{msg.user} has entered the room.</div>
  {/if}
{/each}
