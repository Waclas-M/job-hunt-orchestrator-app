export const claimReq = {

    adminOruser: (c:any) =>{
        const roles = Array.isArray(c.role) ? c.role : [c.role];
        return roles.includes("Admin") || roles.includes("User");
    },
    adminOrLetterOperator: (c:any)=>{
        const roles = Array.isArray(c.role) ? c.role : [c.role];
        // Uwaga na literówkę w "Letter-operartor" (jeśli tak masz w bazie, to zostaw)
        return roles.includes("Letter-operartor") || roles.includes("Admin");
    }
}