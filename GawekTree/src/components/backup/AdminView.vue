<template>
<HeaderComponent header="AdminPanel - GAWEK Tree"></HeaderComponent>
    <div class="post">
        
        <div> {{msg}}</div>
        <div  class="content">
            <table>
                <thead>
                    <tr>
                        <th>id</th>
                        <th>guid </th>
                        <th>name </th>
                        <th>parentGuid</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="forecast in records" :key="forecast.Id">
                        <td>{{ forecast.id }}</td>
                        <td>{{ forecast.guid }}</td>
                        <td>{{ forecast.name }}</td>
                        <td>{{ forecast.parentGuid }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
<script lang="ts">
    import HeaderComponent from '../components/HeaderComponent.vue';

    export default {
        name: 'AdminView',
        props: {
            msg: String
        },
        components:{
            HeaderComponent
        },
        records: null ,
        data() {
            return {
                records: null,
            };
        },

        setup() {
            console.log("setupik");
            return {};
        },

        async created() {
            const response = await fetch("https://localhost:5001/api/Tree");
            this.records = await response.json();
            
            
            
        }
    }
</script>