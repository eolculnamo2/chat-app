<script>
  import { navigate } from "svelte-routing";
  import { onMount } from "svelte";
  import { username, imageUrl } from "../../store/userInformation.js";
  import { HubConnectionBuilder } from "@microsoft/signalr";

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

  connection.on("ReceiveMessage", ($username, message) => {
    let msg = message
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;");
    messages = [...messages, new Message($username, msg)];
  });

  connection.on("EnterChatroom", $username => {});
  connection.start();

  function handleSend() {
    connection.invoke("SendMessage", $username, newMessage).catch(err => {
      return connection.error(err.toString());
    });
  }

  onMount(() => {
    if (!$username.length) {
      navigate("/");
    }
    connection.invoke("AlertEntrance", $username);
    new Message($username, "", MESSAGE_TYPES.ENTRANCE);
    messages = [...messages, { user: $username }];
  });
</script>

<style>
  .enter-chat {
    color: darkgreen;
  }
</style>

<div>{$username}</div>
<div>Message</div>
<input bind:value={newMessage} />
<button on:click={handleSend} type="button">Send</button>

{#each messages as msg}
  {#if msg.type === 'MESSAGE'}
    <div>{msg.user} says: {msg.message}</div>
  {:else}
    <div class="enter-chat">{msg.user} has entered the room.</div>
  {/if}
{/each}
