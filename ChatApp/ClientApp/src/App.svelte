<script>
  import Routes from "./Routes.svelte";
  import { Router } from "svelte-routing";
  export let url = "";
</script>

<Router {url}>
  <Routes />
</Router>
