class TreeService {

    async getObjectByGuid(guid: any) {
        return await fetch("https://localhost:5001/api/Tree/" + guid)
            .then(async response => {
                const data = await response.json();
                if (!response.ok) {
                    const error = (data && data.message) || response.statusText;
                    return Promise.reject(error);
                }
                return data;
            })
            .catch(error => {
                return null;
            });
    }

    async grabChildren(parentGuid: any) {


        return await fetch("https://localhost:5001/api/Tree/child/" + parentGuid)
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

    }
    findRecuParent(guid: any, list: any): TreeNode {
        
        for (const i in list) {
           
            if (list[i].guid == guid) {
               
                return list[i];
            }
            else if (list[i].children.length > 0) {
                const result = this.findRecuParent(guid, list[i].children);
                if (result == null)
                    continue;
                else
                    return result;
            }
        }
        return {name:"", guid:"", children:[], parentGuid:""};

    }

    checkIfExistsInArray(array: TreeNode[], guid: any) {
        for (const i in array) {
            if (array[i].guid === guid) {
                return true;
            }
        }
        return false;

    }

    async skeleton():Promise<TreeNode> {
        const treeData:TreeNode =
        {
            name: "root",
            guid: "00000000-0000-0000-0000-000000000000",
            parentGuid: "00000000-0000-0000-0000-000000000000",
            children: []
        };

        const response = await fetch("https://localhost:5001/api/Tree/Root");
        const mapped = await response.json();
        for (const i in mapped) {
            treeData.children.push({name: mapped[i].name,
                guid: mapped[i].guid,
                parentGuid: mapped[i].parentGuid,
                children: [],});
      
        }
        return treeData;
        
    }

    async getNumberOfChildren(id:any)
    {
       return await fetch("https://localhost:5001/api/Tree/child/count/"+id)
        .then(async response => {
            const data = await response.json();
            if (!response.ok) {
                const error = (data && data.message) || response.statusText;
                return Promise.reject(error);
            }
            return data.message;
        })
        .catch((error) => {
            alert(error);
            return null;
        });
    }



}

export const treeService = new TreeService();