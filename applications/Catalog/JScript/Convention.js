// JScript File

function TranslateField(field)
{
    var lastchar = " ";
    var character = " ";
    var newname = "";
    
    for (i=0;i<=field.length;i++)
    {
        if (lastchar == " "||lastchar=="'")
        {
            if (field.charAt(i)!=" "&&field.charAt(i)!="'")
            {
                character = field.charAt(i).toUpperCase();
                newname = newname + character;
                lastchar = field.charAt(i);
            }
            else{
                newname = newname + field.charAt(i);
                lastchar = " ";
            }
        }
        else{
            newname = newname + field.charAt(i);
            lastchar = field.charAt(i);
        }
    }
    field = newname;
    return field;
}

