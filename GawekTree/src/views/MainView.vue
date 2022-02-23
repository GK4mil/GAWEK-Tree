<template>
<HeaderComponent header="MainPage - GAWEK Tree"></HeaderComponent>
<div class="container">
    <ListComponent> </ListComponent>
</div>
</template>
<script lang="ts">
    import ListComponent from '../components/ListComponent.vue'
    import HeaderComponent from '../components/HeaderComponent.vue';

    export default {
        name: 'MainView',
        props: {
            msg: String
        },
        components : {
            ListComponent,
            HeaderComponent
        },
        records: null ,
        data() {
            return {
                records: null,
            };
        },

        setup() {
           
            return {};
        },

        async created() {
            const response = await fetch("https://localhost:5001/api/Tree");
            this.records = await response.json();
            
            
            
        }
    }
</script>
<style scoped>
.container
{
  margin: 5%;
}
</style>