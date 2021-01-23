using System;

namespace 职责链模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class DutyBuilder
    {
        public DutyBuilder()
        {

        }
    }

    public interface IDuty
    {
        void Approval(AbstractStaff context);
    }

    public class Duty : IDuty
    {

        protected IDuty _NextAudtitor { get; private set; }

        public void SetNext(IDuty nextAuditor)
        {
            this._NextAudtitor = nextAuditor;
        }

        protected void AuditNext(AbstractStaff context)
        {
            if (this._NextAudtitor == null)
            {
                context.AuditResult = false;
                context.AuditRemark = "审批不通过";
            }
            else
            {
                this._NextAudtitor.Approval(context);
            }
        }

        public string Name { get; set; }

        /// <summary>
        /// 通过不通过
        /// </summary>
        public virtual void Approval(AbstractStaff context)
        { }
    }

    public class PM : Duty
    {
        public override void Approval(AbstractStaff context)
        {
            if (context.Hours<=16)
            {
                context.AuditResult = true;
                context.AuditRemark = string.Format("在成功{0}处,请假成功", this.GetType().Name);
                WriteHelper.Write(context.AuditRemark);
            }
            else
            {
                if (this._NextAudtitor is null)
                {
                    context.AuditResult = false;
                    context.AuditRemark = string.Format("在成功{0}处,审批不通过", this.GetType().Name);
                    WriteHelper.Write(context.AuditRemark);
                }
                else
                {
                    this.AuditNext(context);
                }
            }
        }
    }

    public class Director : Duty
    {
        public override void Approval(AbstractStaff context)
        {
            if (context.Hours <= 32)
            {
                context.AuditResult = true;
                context.AuditRemark = string.Format("在成功{0}处,请假成功", this.GetType().Name);
                WriteHelper.Write(context.AuditRemark);
            }
            else
            {
                if (this._NextAudtitor is null)
                {
                    context.AuditResult = false;
                    context.AuditRemark = string.Format("在成功{0}处,审批不通过", this.GetType().Name);
                    WriteHelper.Write(context.AuditRemark);
                }
                else
                {
                    this.AuditNext(context);
                }
            }
        }
    }

    public class Manager : Duty
    {
        public override void Approval(AbstractStaff context)
        {
            if (context.Hours <= 48)
            {
                context.AuditResult = true;
                context.AuditRemark = string.Format("在成功{0}处,请假成功", this.GetType().Name);
                WriteHelper.Write(context.AuditRemark);
            }
            else
            {
                if (this._NextAudtitor is null)
                {
                    context.AuditResult = false;
                    context.AuditRemark = string.Format("在成功{0}处,审批不通过", this.GetType().Name);
                    WriteHelper.Write(context.AuditRemark);
                }
                else
                {
                    this.AuditNext(context);
                }
            }
        }
    }

    public class Chief : Duty
    {
        public override void Approval(AbstractStaff context)
        {
            if (context.Hours <= 72)
            {
                context.AuditResult = true;
                context.AuditRemark = string.Format("在成功{0}处,请假成功", this.GetType().Name);
                WriteHelper.Write(context.AuditRemark);
            }
            else
            {
                if (this._NextAudtitor is null)
                {
                    context.AuditResult = false;
                    context.AuditRemark = string.Format("在成功{0}处,审批不通过", this.GetType().Name);
                    WriteHelper.Write(context.AuditRemark);
                }
                else
                {
                    this.AuditNext(context);
                }
            }
        }
    }

    public class CEO : Duty
    {
        public override void Approval(AbstractStaff context)
        {
            if (context.Hours <= 100)
            {
                context.AuditResult = true;
                context.AuditRemark = string.Format("在成功{0}处,请假成功", this.GetType().Name);
                WriteHelper.Write(context.AuditRemark);
            }
            else
            {
                if (this._NextAudtitor is null)
                {
                    context.AuditResult = false;
                    context.AuditRemark = string.Format("在成功{0}处,审批不通过", this.GetType().Name);
                    WriteHelper.Write(context.AuditRemark);
                }
                else
                {
                    this.AuditNext(context);
                }
            }
        }
    }

    public static class WriteHelper
    {
        public static void Write(string remark)
        {
            Console.WriteLine(remark);
        }
    }


    public abstract class AbstractStaff
    {
        public string Name { get; set; }

        public int Hours { get; set; }

        public bool AuditResult { get; set; }

        public string AuditRemark { get; set; }
    }

    public class Staff : AbstractStaff
    {
        public Staff()
        {
            WriteHelper.Write("我正在被构造");
        }
    }


}
