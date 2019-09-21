<script>
  import { HubConnectionBuilder } from "@microsoft/signalr";

  let user;
  let newMessage;
  let messages = [];

  function Message(user, message) {
    this.user = user;
    this.message = message;
  }

  const connection = new HubConnectionBuilder().withUrl("/chatHub").build();

  connection.on("ReceiveMessage", function(user, message) {
    let msg = message
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;");
    messages = [...messages, new Message(user, msg)];
  });

  connection.start();

  function handleSend() {
    connection.invoke("SendMEssage", user, newMessage).catch(err => {
      return connection.error(err.toString());
    });
  }
</script>

<div>User</div>
<input bind:value={user} />
<div>Message</div>
<input bind:value={newMessage} />
<button on:click={handleSend} type="button">Send</button>

{#each messages as msg}
  <div>{msg.user} says: {msg.message}</div>
{/each}
