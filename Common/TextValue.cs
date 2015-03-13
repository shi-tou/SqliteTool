using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class TextValue
    {
        
        private string text;
        private object value;

        public TextValue(string text, object value)
        {
            this.text = text;
            this.value = value;
        }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }
        /// <summary>
        /// 对应值
        /// </summary>
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
