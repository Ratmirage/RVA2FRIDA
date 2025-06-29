using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RVA2FRIDA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // You can leave this empty
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // You can leave this empty
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // You can leave this empty
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // You can leave this empty
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string rvaSetter = textBox2.Text.Trim(); // RVA SETTER
            string rvaGetter = textBox3.Text.Trim(); // RVA GETTER
            string valueToSet = textBox4.Text.Trim(); // Value to set

            // Make sure RVA starts with 0x
            if (!rvaSetter.StartsWith("0x")) rvaSetter = "0x" + rvaSetter;
            if (!rvaGetter.StartsWith("0x")) rvaGetter = "0x" + rvaGetter;

            string script = $@"// --- GENERIC FRIDA SCRIPT GENERATED AUTOMATICALLY ---
// NOTE: Always use RVA (from 'RVA: 0x...'), not file Offset!
const RVA_SETTER = {rvaSetter};    // RVA of the SETTER function
const RVA_GETTER = {rvaGetter};    // RVA of the GETTER function
const VALUE_TO_SET = {valueToSet}; // Value to force (as integer)

function waitForLib(callback) {{
    const libName = ""libil2cpp.so"";
    let timer = setInterval(() => {{
        let base = Module.findBaseAddress(libName);
        if (base) {{
            clearInterval(timer);
            callback(base);
        }}
    }}, 500);
}}

waitForLib((base) => {{
    console.log(`[!] Base address: ${{base}} (arch: ${{Process.arch}})`);

    // --- SETTER HOOK ---
    let setterAddr = base.add(RVA_SETTER);
    Interceptor.attach(setterAddr, {{
        onEnter(args) {{
            let oldValue = args[0].toInt32();
            args[0] = ptr(VALUE_TO_SET);
            console.log(`[Setter] Called at RVA ${{RVA_SETTER}}. Original arg: ${{oldValue}}, replaced with: ${{VALUE_TO_SET}}`);
        }}
    }});
    console.log(`[+] Listening for SETTER @ ${{setterAddr}}`);

    // --- GETTER HOOK ---
    let getterAddr = base.add(RVA_GETTER);
    Interceptor.attach(getterAddr, {{
        onEnter(args) {{
            console.log(`[Getter] Called at RVA ${{RVA_GETTER}}`);
        }},
        onLeave(retval) {{
            console.log(`[Getter] Returned value: ${{retval.toInt32()}}`);
        }}
    }});
    console.log(`[+] Listening for GETTER @ ${{getterAddr}}`);
}});
";

            // Output script to textBox5
            textBox5.Text = script;
        }
    }
}
