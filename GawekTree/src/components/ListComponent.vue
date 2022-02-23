<template>
    <button @click="showVariable">  Show full tree </button>
    <div>
        <h3>Tree view</h3>
        <ChildComponent :key="showFullTree" :toExpand="showFullTree" @solve="solve" :node="treeData" />
    </div>
</template>

<script lang="ts">
import ChildComponent from './ChildComponent.vue'
import { treeService } from "../services/TreeService";

export default {
    name: "ListComponent",
    components: {
        ChildComponent
    },
    treeData:[],
    showFullTree:false,
    data() {
        return {
           treeData: [],
            showFullTree: false,
        }
    },
    async created(){
        this.treeData = await treeService.skeleton();
    },
    methods:{
        showVariable()
        {
            
            this.showFullTree=!this.showFullTree;
        },
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
                if(children!=null)
                {
                    for(var i in children )
                    {
                        if(!this.checkIfExistsInArray(res.children,children[i].guid))
                            res.children.push({name:children[i].name, guid:children[i].guid, parentGuid: children[i].parentGuid, children: [] });
                    }
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
        
        grabChildren: async function(parentGuid:any){
            

            return await fetch("https://localhost:5001/api/Tree/child/"+parentGuid)
                .then(async response => {
                    const data = await response.json();
                    if (!response.ok) {
                            const error = (data && data.message) || response.statusText;
                            return Promise.reject(error);
                    }
                    return data;
                 })
                .catch(() => {
                    return null;
                 });

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