// JScript File

    function TranslateName(name)
    {
        var lastchar = " ";
        var character = " ";
        var lastchar2 = " ";
        var newname = "";
        
        for (i=0;i<=name.length;i++)
        {
            if (lastchar == " "
               ||(lastchar == "'" && (lastchar2 == "o" || lastchar2 == "O"))
               ||(lastchar == "c" && lastchar2 == "M"))
            {
                if (name.charAt(i)!=" "&&name.charAt(i)!="'")
                {
                    character = name.charAt(i).toUpperCase();
                    newname = newname + character;
                    lastchar = newname.charAt(i);
                    lastchar2 = newname.charAt(i-1);
                }
                else{
                    newname = newname + name.charAt(i);
                    lastchar = newname.charAt(i);
                    lastchar2 = newname.charAt(i-1);
                }
            }
            else{
                newname = newname + name.charAt(i);
                lastchar = newname.charAt(i);
                lastchar2 = newname.charAt(i-1);
            }
        }

        return newname;
    }

    function TranslateField(field)
    {
        var lastchar = " ";
        var lastchar2 = " ";
        var character = " ";
        var newfield = "";
        
        for (i=0;i<=field.length;i++)
        {
            if (lastchar == " "
               || (lastchar == "'" && (lastchar2 == "o" || lastchar2 == "O"))
               || (lastchar == "c" && lastchar2 == "M"))
            {
                if (field.charAt(i)!=" "&&field.charAt(i)!="'")
                {
                    character = field.charAt(i).toUpperCase();
                    newfield = newfield + character;
                    lastchar = newfield.charAt(i);
                    lastchar2 = newfield.charAt(i-1);
                }
                else{
                    newfield = newfield + field.charAt(i);
                    lastchar = newfield.charAt(i);
                    lastchar2 = newfield.charAt(i-1);
                }
            }
            else{
                newfield = newfield + field.charAt(i);
                lastchar = newfield.charAt(i);
                lastchar2 = newfield.charAt(i-1);
            }
        }

        return newfield;
    }

    function TranslateSentence(field)
    {
        var lastchar = " ";
        var lastchar2 = ".";
        var character = " ";
        var nextc = " ";
        var newfield = "";
        
        for (i=0;i<=field.length;i++)
        {
            if (i == field.length)
            {
                nextc = " ";
            }
            else {
                nextc = field.charAt(i+1);
            }
            if ((lastchar == " " && (lastchar2 == "." || lastchar2 == "?" || lastchar2 == "!"))
               || (lastchar == "'" && (lastchar2 == "o" || lastchar2 == "O"))
               || (lastchar == "c" && (lastchar2 == "M" || lastchar2 == "m"))
               || (field.charAt(i) == "i" && ((lastchar == " " || lastchar == "(" || lastchar == "&")
                  && (nextc == " " || nextc == "'" || nextc == "." || nextc == ","
                     || nextc == ";" || nextc == ":" || nextc == "/" || nextc == "?"
                     || nextc == "!" || nextc == "&" || nextc == ")"))))
            {
                if (field.charAt(i)!=" "&&field.charAt(i)!="'")
                {
                    character = field.charAt(i).toUpperCase();
                    newfield = newfield + character;
                    lastchar = newfield.charAt(i);
                    lastchar2 = newfield.charAt(i-1);
                }
                else{
                    newfield = newfield + field.charAt(i);
                    lastchar = newfield.charAt(i);
                    lastchar2 = newfield.charAt(i-1);
                }
            }
            else{
                if ((field.charAt(i) == "m" && field.charAt(i+1) == "c")
                   ||(field.charAt(i) == "o" && field.charAt(i+1) == "'"))
                {
                    character = field.charAt(i).toUpperCase();
                    newfield = newfield + character;
                }
                else {
                    newfield = newfield + field.charAt(i);
                }
                lastchar = newfield.charAt(i);
                lastchar2 = newfield.charAt(i-1);
            }
        }

        return newfield;
    }
