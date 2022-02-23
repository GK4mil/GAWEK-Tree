<template>
    <button @click="showVariable">  Show full tree </button>
    <div>
        <h3>Tree view</h3>
        <ChildComponent @solve="solve" :node="treeData" />
    </div>
</template>

<script lang="ts">
import ChildComponent from './ChildComponent.vue'
export default {
    name: "ListComponent",
    components: {
        ChildComponent
    },
    treeData:[],
    data() {
        return {
            treeData : {
                name: "Main",
                guid: "00000000-0000-0000-0000-000000000000",
                parentGuid: "00000000-0000-0000-0000-000000000000",
                children: [
                    
                ]
            }
        }
    },
    async created(){
        console.log(this.treeData);
        const response = await fetch("https://localhost:5001/api/Tree/Root");
        const mapped = await response.json();
        for(var i in mapped)
        {
            this.treeData.children.push({name:mapped[i].name, guid:mapped[i].guid, parentGuid: mapped[i].parentGuid, children: [] });
        }
           
    },
    methods:{
        solve: async function(id:any){
            console.log("to solve");
           console.log(id);
            var res =this.findRecuParent(id,[this.treeData]);
            console.log("res");
            console.log(res);
            if(res!==null)
            {
                res.children=[];
                var children = await this.grabChildren(id)
                for(var i in children )
                {
                    if(!this.checkIfExistsInArray(res.children,children[i].guid))
                        res.children.push({name:children[i].name, guid:children[i].guid, parentGuid: children[i].parentGuid, children: [] });
                }
                
            }
                
        },
        checkIfExistsInArray:function (array: [], guid:any)
        {
            for(var i in array)
            {
                //console.log(array[i]);
                
                if(array[i].guid===guid)
                {
                    return true;
                }
            }
            return false;
  
        },
        showVariable()
        {
            console.log(this.treeData);
        },
        grabChildren: async function(parentGuid:any){
            const response = await fetch("https://localhost:5001/api/Tree/child/"+parentGuid);
            const mapped = await response.json();
            return mapped;
        },
        update: function(){
                console.log("up");
                this.treeData.children.push({name:"updated"});
        },
        findRecuParent(guid:any, list :any):[]
        {
            console.log("recu");
            console.log(list);
            
            
            for(var i in list)
            { 
                console.log("reculoop");
               console.log(guid);
               console.log(list[i].guid);
               console.log(list[i].children.length);
                if(list[i].guid==guid)
                {
                    console.log("found");
                    console.log(list[i].guid);
                    return list[i];
                }
                else if(list[i].children.length>0)
                {
                    var result= this.findRecuParent(guid,list[i].children);
                    if(result==null)
                        continue;
                    else
                        return result;
                }
                    

               
            }
            return null;
            
            /*list.children.forEach((obj)=>{
                obj2=obj;
                if(obj.guid===guid)
                {
                    
                    return obj;
                }
                this.findRecuParent(guid,obj2);
            })*/
            
         
        }

    }
}
</script>