SELECT users.name, users.surname, departman.depNam from users inner join departman on users.depID = departman.depID where users.depID='1'
