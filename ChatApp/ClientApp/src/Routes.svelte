<script>
  import { Route } from "svelte-routing";
  import Authentication from "./pages/Authentication.svelte";
  import Chat from "./pages/Chat.svelte";
  import NotFound from "./pages/NotFound.svelte";
</script>

<Route path="/" component={Authentication} />
<Route path="/chat" component={Chat} />
<Route component={NotFound} />
