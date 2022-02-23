<template>
    <div :key="record.guid" v-for="record in records">
    <ChildComponent :paramObj=record />
  </div>
</template>

<script lang="ts">
    import ChildComponent from './ChildComponent.vue'

    export default {
        name: 'ListComponent',
        props: {
            msg: String
        },
        records: null,
        data() {
            return {
                records: null,
            };
        },
        components:
        {
            ChildComponent,
        },       

        async created() {
            const response = await fetch("https://localhost:5001/api/Tree/Root");
            this.records = await response.json();
            
            
            
        }
    }
</script>

<style scoped>
div
{
    margin-left: 5%;
}
</style>