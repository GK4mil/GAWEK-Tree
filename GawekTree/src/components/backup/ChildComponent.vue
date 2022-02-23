<template>
    <div @click="records=getChildren(paramObj.guid); shouldVisible=!shouldVisible">{{paramObj.name}} | {{paramObj.guid}} | {{paramObj.parentGuid}} </div>
    <div :key="records">
    <div v-for="record in records" :key="record.guid"  >
        <ChildComponent :paramObj=record ></ChildComponent>
    </div>
    </div>
    
</template>
<script lang="ts">

export default {
    name: 'ChildComponent',
    props: {
        paramObj : Object,
    },
    records: null,
    data(){
        return {
            
            records : []
        }
    },
    created()
    {
      
    },
    methods:
    {
         async getChildren(id: any)
        {
            console.log(id);
               const response = await fetch("https://localhost:5001/api/Tree/child/"+id);            
               return await response.json(); 
                
        }
    }

    
}
</script>

<style scoped>
div
{
    margin-left: 5%;
}
.child
{
    margin-left: 5%;
}
</style>
